using ECommerceAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod)
        {
            string newFileName = await Task.Run(async () =>
            {
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                bool fileExists = false;
                int indexNo = 0;
                do
                {
                    //if (File.Exists($"{path}\\{newFileName}"))
                    if (hasFileMethod(pathOrContainerName, newFileName))
                        {
                        fileExists = true;
                        indexNo++;
                        newFileName = $"{NameOperation.CharacterRegulatory(oldName + "-" + indexNo)}{extension}";
                    }
                    else
                    {
                        fileExists = false;
                    }
                } while (fileExists);

                return newFileName;
            });

            return newFileName;
        }
    }
}
