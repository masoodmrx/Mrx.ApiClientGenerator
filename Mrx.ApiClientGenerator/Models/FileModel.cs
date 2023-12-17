using System.Collections.Generic;

namespace Mrx.ApiClientGenerator.Models
{
    public class FileModel
    {
        public int? SelectedIndexProfile { get; set; }
        public List<ProfileModel> Profiles { get; set; }
    }
}
