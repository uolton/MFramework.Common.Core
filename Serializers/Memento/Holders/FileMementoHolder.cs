using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFramework.Common.Core.Serializers.Memento.Holders
{
    public class FileMementoHolder<TOriginator> : MementoHolderBase<TOriginator>
        where TOriginator : class,IMementoOriginator<ExpandoObject>, new()
    {
        private Func<TOriginator,FileInfo> _fileLocator;

        public FileMementoHolder(string path, Func<dynamic, ExpandoObject> loadSetter, Func<TOriginator, TOriginator> createSetter)
            : this(new FileInfo(path), loadSetter, createSetter)
        {

        }
        public FileMementoHolder(FileInfo file, Func<dynamic, ExpandoObject> loadSetter, Func<TOriginator, TOriginator> createSetter)
            : this((o) => file, loadSetter, createSetter)
        {

        }

        public FileMementoHolder(Func<TOriginator, FileInfo> filelocator, Func<dynamic, ExpandoObject> loadSetter, Func<TOriginator, TOriginator> createSetter)
            : base(loadSetter, createSetter)
        {
            _fileLocator = filelocator;
        }
        public FileMementoHolder(string path, Func<dynamic, ExpandoObject> loadSetter)
            : this(new FileInfo(path), loadSetter )
        {
        
        }
        public FileMementoHolder(FileInfo file, Func<dynamic, ExpandoObject> loadSetter)
            : this((o) => file,  loadSetter)
        {
        
        }

        public FileMementoHolder(Func<TOriginator, FileInfo> filelocator, Func<dynamic, ExpandoObject> loadSetter):base(loadSetter)
        {
            _fileLocator = filelocator;
        }
        
        
        
        public FileMementoHolder(string  path):this(new FileInfo(path))
        {
        
        }
        public FileMementoHolder(FileInfo file):this((o) => file)
        {
        
        }

        public FileMementoHolder(Func<TOriginator,FileInfo> filelocator):base()
        {
            _fileLocator = filelocator;
        }
        protected override Stream GetStreamToWrite(TOriginator originator)
        {
            return(_fileLocator(originator).OpenWrite());
        }

        protected override Stream GetStreamToRead(TOriginator originator)
        {
            return  _fileLocator(originator).OpenRead();
        }

        protected override void CloseStream(Stream stream)
        {
            stream.Flush();
            stream.Close();
            stream.Dispose();
        }
    }
}
