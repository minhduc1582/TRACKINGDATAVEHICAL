using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop_api.Helpers
{
    public class ConvertBase64toFile
    {
        public static IFormFile Base64ToImage(string ImageBase64)
        {
            try{
                string guid = Guid.NewGuid().ToString();
                byte[] bytes = Convert.FromBase64String(ImageBase64);
                MemoryStream stream = new MemoryStream(bytes);
                IFormFile file = new FormFile(stream, 0, bytes.Length, guid, guid);
                    
                return file;
            }
            catch(Exception ex){
                throw ex;
            }
            
        }
    }
}