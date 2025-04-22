using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;
using System.IO.Compression;

namespace ObjectGrabberBuildTasks
{
    public class ZipTask : Task
    {
        [Required]
        public ITaskItem[] Files { get; set; }

        [Required]
        public string OutputName { get; set; }

        [Required]
        public string OutputLocation { get; set; }

        [Output]
        public string OutputZipFile { get; set; }
        
        public override bool Execute()
        {
            string directory_to_zip = Path.Combine(OutputLocation, OutputName);

            // Clear out anything that may have been left in there
            if (Directory.Exists(directory_to_zip))
            {
                Directory.Delete(directory_to_zip, true); 
            }
            Directory.CreateDirectory(directory_to_zip);

            foreach (ITaskItem input_file in Files)
            {
                string file_string = input_file.ItemSpec;
                if (File.Exists(file_string))
                {
                    string file_to_zip = Path.Combine(directory_to_zip, Path.GetFileName(file_string));
                    File.Copy(file_string, file_to_zip, true);
                    Log.LogMessage("File copied: " + file_string);
                }
                else
                {
                    Log.LogError("File does not exist: " + file_string);
                    return false;
                }
            }

            OutputZipFile = directory_to_zip + ".zip";
            if (File.Exists(OutputZipFile))
                File.Delete(OutputZipFile);
            ZipFile.CreateFromDirectory(directory_to_zip, OutputZipFile);

            Directory.Delete(directory_to_zip, true);
            
            return true;
        }
    }
}
