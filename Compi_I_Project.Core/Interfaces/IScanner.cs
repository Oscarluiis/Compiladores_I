using System;
using Compi_I_Project.Core.Models;

namespace Compi_I_Project.Core.Interfaces
{
    public interface IScanner
    {
        Token GetNextToken();
    }
}
