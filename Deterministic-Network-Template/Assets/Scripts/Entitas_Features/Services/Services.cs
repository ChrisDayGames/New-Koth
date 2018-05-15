using System;

public class Services {

	public static Services singleton = new Services();

	public void Initialize(Contexts contexts) {
		
		EntityService.singleton.Initialize (contexts);
		AlphaService.singleton.Initialize (contexts);

		//ViewService.singleton.Initialize(contexts);
	}
}
