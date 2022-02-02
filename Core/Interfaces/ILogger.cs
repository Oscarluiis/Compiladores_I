using System;
namespace Compi_Project.Core.Interfaces
{
    public interface ILogger
    {
        void Error(string message);

        void Info(string message);
    }
}
