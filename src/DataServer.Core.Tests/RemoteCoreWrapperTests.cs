namespace DataServer.Core.Tests
{
	[TestClass]
	public class RemoteCoreWrapperTests
	{
		public RemoteCoreWrapper Initialize()
		{
			return new RemoteCoreWrapper();
		}

		[TestMethod]
		public void CreateInstance()
		{
			RemoteCoreWrapper remoteCoreWrapper = Initialize();
		}
	}
}
