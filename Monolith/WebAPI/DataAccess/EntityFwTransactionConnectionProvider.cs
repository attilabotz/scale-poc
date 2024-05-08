using System;
using Paramore.Brighter;

namespace WebAPI.DataAccess
{
    public class EntityFwTransactionConnectionProvider : IAmABoxTransactionConnectionProvider
    {
        // debugging purposes
        private string Name = Ulid.NewUlid().ToString();
        
        public readonly NoteContext Context;

        public EntityFwTransactionConnectionProvider(NoteContext context)
        {
            Context = context;
        }

        public void Open()
        {
            Context.Database.BeginTransaction();
        }

        public void Close()
        {
            Context.Database.CurrentTransaction.Commit();
        }

        public void Rollback()
        {
            Context.Database.CurrentTransaction.Rollback();
        }
    }
}