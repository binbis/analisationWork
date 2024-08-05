//.
//script.setup(trayIcon: true, sleepExit: true);
//..

namespace CSLight {
	class Program {
		static void Main() {
			opt.mouse.MoveSpeed = opt.key.KeySpeed = opt.key.TextSpeed = 10;
			
			// Знаходить та активує вікно якщо воно звернуте 
			var w = wnd.find(0, "Delta Monitor - Google Chrome", "Chrome_WidgetWin_1").Activate();
			var buttonFilterWindow = w.Elm["web:LISTITEM", prop: "@data-testid=filters"].Find(1);
			buttonFilterWindow.PostClick(2);
			wait.ms(400);
			var buttonAllFilter = w.Elm["web:PAGETAB", "Усі фільтри", new("@role=tab", "@type=button")].Find(2);
			buttonAllFilter.PostClick(2);
			wait.ms(400);
			var deltaGroupZona = w.Elm["web:STATICTEXT", "Зона"].Find(1);
			deltaGroupZona.PostClick(2);
			wait.ms(400);
			var deltaRadioButtonAllMap = w.Elm["web:RADIOBUTTON", "Уся карта", "@data-testid=all-map-radio"].Find(1);
			deltaRadioButtonAllMap.PostClick(2);
			wait.ms(400);
			var deltaRadioButtonNewArea = w.Elm["web:RADIOBUTTON", "Нова зона", "@data-testid=new-zone-radio"].Find(1);
			deltaRadioButtonNewArea.PostClick(2);
			wait.ms(400);
			//var daltaButtonAccept = w.Elm["web:BUTTON", "Застосувати", "@data-testid=save-button"].Find(1);
			//daltaButtonAccept.PostClick(2);
			//wait.ms(400);
		}
	}
}
