using System;
namespace Compi_I_Project.Core.Interfaces
{
    public interface ILogger
    {
        void Error(string message);

        void Info(string message);
    }
}
