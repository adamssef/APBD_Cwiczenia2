using System;

namespace LegacyApp
{
    public class UserService
    {
        public UserService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        IClientRepository _clientRepository;
        
        

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (!isValidatedPositively(firstName, lastName, email, dateOfBirth, user, client))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool isUserDataValid(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool isUserEmailValid(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }
           
            return true;
        }

        private bool isUserAgeValid(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        private bool hasNotSmallCreditLimit(User user, Client client)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        private bool isValidatedPositively(string firstName, string lastName, string email, DateTime dateOfBirth, User user, Client client)
        {
            if (!isUserDataValid(firstName, lastName))
            {
                return false;
            }

            if (!isUserEmailValid(email))
            {
                return false;
            }

            if (!isUserAgeValid(dateOfBirth))
            {
                return false;
            }
            

            if (!hasNotSmallCreditLimit(user, client))
            {
                return false;
            }

            return true;

        }
    }
}
