using System;

namespace LegacyApp;

public interface IUserCreditService
{
    public int GetCreditLimit(string lastName, DateTime dateOfBirth);

    public void Dispose();
}