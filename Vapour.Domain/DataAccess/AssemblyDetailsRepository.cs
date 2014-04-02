using Vapour.Domain.Interfaces;

namespace Vapour.Domain.DataAccess
{
    public class AssemblyDetailsRepository : IAssemblyDetailsRepository
    {
        private readonly IDatabaseSession _databaseSession;

        public AssemblyDetailsRepository(IDatabaseSession databaseSession)
        {
            _databaseSession = databaseSession;
        }

        public AssemblyDetailsRepository() : this(new DatabaseSession())
        {
        }

        public AssemblyDetails Save(AssemblyDetails assemblyDetails)
        {
            return _databaseSession.Insert<AssemblyDetails>(assemblyDetails, VapourCollections.AssemblyDetails);
        }

        public string GetPathFor(string appName)
        {
            throw new System.NotImplementedException();
        }
    }
}