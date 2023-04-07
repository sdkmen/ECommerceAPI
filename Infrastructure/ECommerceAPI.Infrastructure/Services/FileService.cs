using ECommerceAPI.Infrastructure.Operations;

namespace ECommerceAPI.Infrastructure.Services
{
    public class FileService
    {
        async Task<string> FileRenameAsync(string path, string fileName)
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
                    if (File.Exists($"{path}\\{newFileName}"))
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
