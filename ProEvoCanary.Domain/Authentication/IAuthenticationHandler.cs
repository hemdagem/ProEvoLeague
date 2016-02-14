﻿namespace ProEvoCanary.Authentication
{
    public interface IAuthenticationHandler
    {
        void SignIn(string name, string role, int userId);
        void SignOut();
    }
}