// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    function addChatRoomToList(chatRoom) {
        $("#chatRoomsList").append($("<option value=" + chatRoom + ">" + chatRoom + "</option>"));
    }

    function addMessageToChatWindow(message) {
        $("#chatWindow").val($("#chatWindow").val() + message + "\n");
    }

    function clearChatRoomForRoomChange(chatRoom) {
        $("#chatWindow").val("");
        addMessageToChatWindow("Welcome to " + chatRoom);
    }

    function setupChat() {
        var chat = $.connection.chat;
        chat.client.addChatRoom = function (chatRoom) {
            var currentChatRoom = chat.state.currentChatRoom;
            addChatRoomToList(chatRoom);
            if (chatRoom == currentChatRoom) {
                $("#chatRoomsList").val(currentChatRoom);
                addMessageToChatWindow("Welcome to " + currentChatRoom);
            }
        }

        chat.client.addMessage = function (room, message) {
            if (room === chat.state.currentChatRoom) {
                addMessageToChatWindow(message);
            }
        }

        $.connection.hub.url = "http://localhost:8181/signalr";
        $.connection.hub.start().done(function () {
            $("#chatWindow").val("Connected\n");
            
            chat.state.currentChatRoom = "Lobby";
            chat.server.join("Lobby").fail(function (e) {
                addMessageToChatWindow(e);
            });
            $("#sendButton").click(function () {
                chat.server.send($("#messageTextBox").val());
                $("#messageTextBox").val("")

            });

            $("#createChatRoomButton").click(function () {
                chat.server.createChatRoom($("#chatRoomTextBox").val());
                $("#chatRoomTextBox").val("")
            });

            $("#chatRoomsList").change(function () {
                var currentChatRoom = $("#chatRoomsList option:selected").val();
                chat.state.currentChatRoom = currentChatRoom;
                chat.server.join(currentChatRoom);
                clearChatRoomForRoomChange(currentChatRoom);
            });
        }).fail(function (e) {
            var dialog = new Windows.UI.Popups.MessageDialog("Couldn't connect");
            dialog.showAsync();
        });
    }


    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            setupChat();

            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        // args.setPromise().
    };

    app.start();
})();
