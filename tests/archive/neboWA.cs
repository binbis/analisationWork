/*


*/

using System.Windows;
using System.Windows.Controls;

namespace CSLight {
	class Program {
		static void Main() {
			
			while (true) { 
				string dateTimeNow = DateTime.Now.ToString("dd.mm.yyyy * hh:mm:ss"); // поточний час
				
				// вікно
				var dambaWindow = wnd.find(1, "Damba - Google Chrome", "Chrome_WidgetWin_1").Activate();
				dambaWindow.Elm["web:DOCUMENT", "Damba"].Find(1)/*image:WkJNGzUEAOS+/tAJ3l3WhpiAt/jEDagNdRVq/1u38G9qTFxTP5r+Xt+e/jehaP0BjnbDisLRaFrBbRitlEiDf2+091ekSKK77eQ6fhFO0tCCpYAmoAeolZWAw36iMvs/uZKWwKVb1+HO0/sgnqiF5eVFeHr8EvjVSTjzgsP23g5w8REM9PfAPWEDLj68DeKxCHdfP4Nrrx4BY8N8lE5iw76dtUPhGYUvWaeDg4siyDOlLtgXVF2kIulCRqkq66KcAMP6k7+n6TUcmIjIBZyJVBUnZDr3JclV7nR/dVDjHL1atb0zZC/62d/YoYtHcSHIWPhIXXAP7vuCfvc6VWnvk08Aj+A9h6uSrFRF1f+6nwW8hWGLU4YLLB1OVu6lsZg8zLnZODLkgmXSucAYYvLLHxjmGf8/sWGG7+0gwwFKTZm1iNPvWP4dkHUXMmP6bxjhJ7+sfff9/7ue6f/v3v97R2uG0v/cE1HUmJ5UvVQv+0RWY1IPxvr/9+hA0Hu1YjqZNAkmhDS0kbE2QsYnDuIs+dmfRK0tOLhJwS/Ypwn6k54pZnywJtSH+DURFd/g0FBz+p4pY3A+JNRXRYvUaQ1cfMGvzrdpNWl9NficxSx+q6fb+bPtavO6crGyqnoDDAA=*/.GetRect(out var r, false);
				wait.ms(400);
				using (var b = CaptureScreen.Image(r)) {
					new clipboardData().AddImage(b).SetClipboard();
				}
				wait.ms(500);
				var whatsAppWindow = wnd.find(1, "WhatsApp", "ApplicationFrameWindow").Activate();
				var fielEnterdWhatsApp = whatsAppWindow.Elm["WINDOW", "WhatsApp", "class=Windows.UI.Core.CoreWindow", EFFlags.UIA].Find(1);
				fielEnterdWhatsApp.PostClick();
				keys.send("Ctrl+V");
				keys.send("0");
				wait.s(8);
				keys.send("Enter");
				
				Console.WriteLine($"{dateTimeNow} - спроба небо *");
				
				wait.s(600);
			}
			
		}
	}
}
