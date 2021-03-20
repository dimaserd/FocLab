//В данном файле переопределются входные элементы основного приложения
//До вызова переопределений библиотека CrocoAppCore должна быть объявлена
CrocoAppCore.InitFields();


//Вызываю отрисовку обобщенных форм на UI
CrocoAppCore.GenericInterfaceHelper.FormHelper.DrawForms();

setTimeout(() => {
    NotificationWorker.GetNotification();
}, 1000);