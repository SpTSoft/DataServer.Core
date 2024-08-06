using DataServer.Core.Access;
using DataServer.Core.Auth;
using DataServer.Core.Database;
using DataServer.Core.IO;
using DataServer.Core.Locking;
using DataServer.Core.Logging;
using DataServer.Core.MEF;
using DataServer.Core.Modeling;
using DataServer.Core.Net;
using DataServer.Core.Net.Args;
using DataServer.Core.Net.Settings;
using DataServer.Core.Notifications;

namespace DataServer.Core.Tests
{
	[TestClass]
	public class LocalCoreWrapperTests
	{
		public LocalCoreWrapper Initialize()
		{
			IModuleCompositor moduleCompositor = new ModuleCompositor(new Loader(new Observer(new FileObserver())));
			IDBGate dBGate = new DBGate();
			IAccessCore accessCore = new AccessCore();

			ILogger logger = new NullLogger();
			IGatewayListenerArgsFactory gatewayListenerArgsFactory = new GatewayListenerArgsFactory();

			IGatewayInteractionStorage notificationsgatewayInteractionStorage = new GatewayInteractionStorage();
			IGatewayRouter notificationsgatewayRouter = new GatewayRouter();
			IGatewaySender notificationsgatewaySender = new GatewaySender();
			IGatewayListenerSettings notificationsgatewaygatewayListenerSettings = new GatewayListenerSettings(System.Net.IPAddress.Any, NetHelper.GetAvailablePort());
			IGatewayListener notificationsgatewayListener = new GatewayListener(notificationsgatewaygatewayListenerSettings, gatewayListenerArgsFactory, logger);
			IGateway notificationsGateway = new Gateway(notificationsgatewayListener, notificationsgatewayInteractionStorage, notificationsgatewayRouter, notificationsgatewaySender);

			INotificationsService notificationsService = new NotificationsService(notificationsGateway);

			ILockerService lockerService = new LockerService(notificationsService);

			IGatewayInteractionStorage gatewayInteractionStorage = new GatewayInteractionStorage();
			IGatewayRouter gatewayRouter = new GatewayRouter();
			IGatewaySender gatewaySender = new GatewaySender();
			IGatewayListenerSettings gatewayListenerSettings = new GatewayListenerSettings(System.Net.IPAddress.Any, NetHelper.GetAvailablePort());
			IGatewayListener gatewayListener = new GatewayListener(gatewayListenerSettings, gatewayListenerArgsFactory, logger);
			IGateway authGateway = new Gateway(gatewayListener, gatewayInteractionStorage, gatewayRouter, gatewaySender);
			IAuthCore authCore = new AuthCore();
			IAuthGate authGate = new AuthGate(authGateway, authCore);
			IAccessGate accessGate = new AccessGate(accessCore, lockerService, authGate);

			return new LocalCoreWrapper(moduleCompositor, dBGate, accessGate);
		}

		[TestMethod]
		public void CreateInstance()
		{
			LocalCoreWrapper remoteCoreWrapper = Initialize();
		}
	}
}
