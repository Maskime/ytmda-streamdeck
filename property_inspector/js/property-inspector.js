// global websocket, used to communicate from/to Stream Deck software
// as well as some info about our plugin, as sent by Stream Deck software 
var websocket = null,
    uuid = null,
    inInfo = null,
    actionInfo = {},
    settingsModel = {
        Host: "localhost",
        Port: "9863",
        Password: ""
    };

function connectElgatoStreamDeckSocket(inPort, inUUID, inRegisterEvent, inInfo, inActionInfo) {
    uuid = inUUID;
    actionInfo = JSON.parse(inActionInfo);
    inInfo = JSON.parse(inInfo);
    websocket = new WebSocket('ws://localhost:' + inPort);

    //initialize values
    if (actionInfo.payload.settings.settingsModel) {
        settingsModel.Host = actionInfo.payload.settings.settingsModel.Host;
        settingsModel.Port = actionInfo.payload.settings.settingsModel.Port;
        settingsModel.Password = actionInfo.payload.settings.settingsModel.Password;
    }
    document.getElementById('txtHost').value = settingsModel.Host;
    document.getElementById('txtPort').value = settingsModel.Port;
    document.getElementById('txtPassword').value = settingsModel.Password;

    websocket.onopen = function () {
        var json = {event: inRegisterEvent, uuid: inUUID};
        // register property inspector to Stream Deck
        websocket.send(JSON.stringify(json));
    };

    websocket.onmessage = function (evt) {
        // Received message from Stream Deck
        let jsonObj = JSON.parse(evt.data);
        let sdEvent = jsonObj['event'];
        let updateHtml = false;
        switch (sdEvent) {
            case "didReceiveSettings":
                if (!jsonObj.payload.settings.settingsModel) {
                    break;
                }
                settingsModel.Host = jsonObj.payload.settings.settingsModel.Host;
                settingsModel.Port = jsonObj.payload.settings.settingsModel.Port;
                settingsModel.Password = jsonObj.payload.settings.settingsModel.Password;
                updateHtml = true;
                break;
            case "didReceiveGlobalSettings":
                if (!jsonObj.payload.settings.settingsModel) {
                    break;
                }
                settingsModel.Host = jsonObj.payload.settings.settingsModel.Host;
                settingsModel.Port = jsonObj.payload.settings.settingsModel.Port;
                settingsModel.Password = jsonObj.payload.settings.settingsModel.Password;
                updateHtml = true;
                break;
            default:
                updateHtml = false;
                break;
        }
        if (updateHtml) {
            document.getElementById('txtHost').value = settingsModel.Host;
            document.getElementById('txtPort').value = settingsModel.Port;
            document.getElementById('txtPassword').value = settingsModel.Password;
        }
    };
}

function SendSettings(settings) {
    var json = {
        "event": "setSettings",
        "context": uuid,
        "payload": {
            "settingsModel": settings
        }
    };
    websocket.send(JSON.stringify(json));
}

const setSettings = (value, param) => {
    if (!websocket) {
        return;
    }
    settingsModel[param] = value;
    SendSettings(settingsModel);
};

const setAllSettings = () => {
    if (!websocket) {
        return;
    }
    settingsModel.Host = document.getElementById('txtHost').value;
    settingsModel.Port = document.getElementById('txtPort').value;
    settingsModel.Password = document.getElementById('txtPassword').value;
    SendSettings(settingsModel);
}