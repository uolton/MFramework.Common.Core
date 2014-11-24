namespace MFramework.Common.Core.Streams.Processors
{
    public abstract class  StreamProcessorBase:IStreamProcessor,IStreamPipe
    {

        private IStreamProcessor _next;
        protected IStreamProcessor _prev;
        private bool _hasBeenClosed = false;

        public  abstract void Bind(IStreamFilterRoleWriter writer);
        public abstract void Bind(IStreamFilterRoleReader reader);
        
        public IStreamProcessor Attach(IStreamProcessor p)
        {
            DoBind(p);
            _next = p;
            return this;
        }

        protected virtual void DoFlush() { }
        protected abstract void DoClose();
         public void Flush()
        {
            DoFlush();
             if (_next != null)
             {
                 _next.Flush();
                 DoClose();
             }
        }

        public void Close()
        {
            if (_hasBeenClosed) return;
            _hasBeenClosed = true;
            if (_prev != null)
            {
                _prev.Close();
                
            }
            else
            {
                Flush();
            }

        }
        protected virtual void DoBind(IStreamProcessor p)
        {
            
            if (this is IStreamFilterRoleWriter) BindAsWriterTo((IStreamPipe)p);
            if (p is IStreamFilterRoleReader)    BindAsReaderFrom((IStreamPipe)p);
        }
        protected void BindAsWriterTo(IStreamPipe p)
        {
        
            
            p.Bind((IStreamFilterRoleWriter) this);
            
        }

        protected void BindAsReaderFrom(IStreamPipe p)
        {
        

            p.Bind((IStreamFilterRoleReader)this);
        }

        
    }
}
