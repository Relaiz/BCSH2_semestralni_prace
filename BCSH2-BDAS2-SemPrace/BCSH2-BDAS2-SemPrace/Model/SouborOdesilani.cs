using BCSH2_BDAS2_SemPrace.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class SouborOdesilani : ViewModelBase
    {
        private int _idFile;
        private string _nazevFile;
        private byte[] _fileContent;
        private string _prijemceFile;
        private string _formatFile;
        private int _zameIdZamestnanec;
        private int _klientIdKlient;

        public int IdFile
        {
            get { return _idFile; }
            set { _idFile = value; OnPropertyChanged(nameof(IdFile)); }
        }

        public string NazevFile
        {
            get { return _nazevFile; }
            set { _nazevFile = value; OnPropertyChanged(nameof(NazevFile)); }
        }

        public byte[] FileContent
        {
            get { return _fileContent; }
            set { _fileContent = value; OnPropertyChanged(nameof(FileContent)); }
        }

        public string PrijemceFile
        {
            get { return _prijemceFile; }
            set { _prijemceFile = value; OnPropertyChanged(nameof(PrijemceFile)); }
        }

        public string FormatFile
        {
            get { return _formatFile; }
            set { _formatFile = value; OnPropertyChanged(nameof(FormatFile)); }
        }

        public int ZameIdZamestnanec
        {
            get { return _zameIdZamestnanec; }
            set { _zameIdZamestnanec = value; OnPropertyChanged(nameof(ZameIdZamestnanec)); }
        }

        public int KlientIdKlient
        {
            get { return _klientIdKlient; }
            set { _klientIdKlient = value; OnPropertyChanged(nameof(KlientIdKlient)); }
        }
    }

}
