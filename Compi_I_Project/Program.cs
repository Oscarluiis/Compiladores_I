using System;
using System.IO;
using Compi_I_Project.Core.Models;
using Compi_I_Project.Infrastructure;
using Compi_I_Project.Lexer;

namespace Compi_I_Project
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var fileContent = File.ReadAllText("../../test.txt");
            var logger = new Logger();
            var scanner = new Scanner(new Input(fileContent), logger);
            var token = scanner.GetNextToken();
            while (token.TokenType != TokenType.OpEOF)
            {
                logger.Info(token.ToString());
                token = scanner.GetNextToken();
            }

        }
    }
}
