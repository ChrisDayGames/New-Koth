using System.Runtime.Serialization;

namespace Persistence {

	public interface ISaveable  {

		string name {get;}
		string extension {get;}
		string path {get;}

	}

}