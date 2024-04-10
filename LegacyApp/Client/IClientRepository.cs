namespace LegacyApp;

public interface IClientRepository
{
    internal Client GetById(int clientId);
}