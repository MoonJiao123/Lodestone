
namespace Andtech.SceneManagement {

	public class SceneLoader : Singleton<SceneLoader> {
		private static Bootstrapper Bootstrapper;

		public static void Mount(Bootstrapper bootstrapper) {
			Bootstrapper lastBootstrapper = Bootstrapper;
			
			if (lastBootstrapper != null)
				lastBootstrapper.OnDismount();

			Bootstrapper = null;
			if (bootstrapper == null)
				return;

			bootstrapper.OnMount();
		}
	}
}
