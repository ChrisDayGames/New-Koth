using System.Collections.Generic;

namespace Persistence {

	public interface ISaveLoad {

		void Save (ISaveable data);

		ISaveable Load (string path);

		ISaveable[] LoadAll (string path, string extension);

	}

}