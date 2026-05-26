using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mrx.ApiClientGenerator.Models
{
    /// <summary>
    /// yourComboBox.DataSource =  Enum.GetValues(typeof(YourEnum));
    /// YourEnum enum = (YourEnum) yourComboBox.SelectedItem;
    /// yourComboBox.SelectedItem = YourEnem.Foo;
    /// </summary>
    public enum Language
    {
        TypeScript,
        CSharp,
        Dart
    }
}
