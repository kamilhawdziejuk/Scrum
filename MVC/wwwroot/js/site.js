// Write your JavaScript code.
var transportType = signalR.TransportType.WebSockets;

//can also be ServerSentEvents or LongPolling

var logger = new signalR.ConsoleLogger(signalR.LogLevel.Information);
var chatHub = new signalR.HttpConnection(`http://${document.location.host}/scrum`, { transport: transportType, logger: logger });
var chatConnection = new signalR.HubConnection(chatHub, logger);

chatConnection.onClosed = e => {
   console.log('connection closed');
};

chatConnection.on('Send', (message) => {
    console.log('received message');
});

chatConnection.start().catch(err => {
    console.log('connection error');
});

function send(message) {
    chatConnection.invoke('Send', message);
}