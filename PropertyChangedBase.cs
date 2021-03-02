using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LogViewExample
{
    [Obsolete("Remove this later. Instead use MVVMlite framework")]
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}