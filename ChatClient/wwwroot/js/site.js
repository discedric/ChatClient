document.addEventListener('DOMContentLoaded', function () {
    // Populate the setUserForm with the Chatter cookie value
    var user = getCookie("Chatter");
    if (user) {
        document.getElementById('User').value = user;
    }

    // Handle set user form submission
    document.getElementById('setUserForm').addEventListener('submit', function (e) {
        e.preventDefault();
        var user = document.getElementById('User').value;
        if (user === '') {
            user = 'Anonymous';
        }
        document.cookie = "Chatter=" + user + "; path=/";
    });

    // Handle send message form submission
    document.querySelector('.sendMessage').addEventListener('submit', function (e) {
        e.preventDefault();
        var message = document.getElementById('Message').value;
        var channel = document.getElementById('Channel').textContent; // Use .textContent to get the channel name from the <h1> element
        var user = getCookie("Chatter");

        // Validation
        if (message.trim() === '') {
            alert("Message cannot be empty.");
            return;
        }
        if (!user || user.trim() === '') {
            alert("Author cannot be empty.");
            return;
        }

        fetch('/Home/SendMessage', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ author: user, message: message, channel: channel })
        }).then(response => {
            if (response.ok) {
                document.getElementById('Message').value = '';
                refreshMessages(channel);
            }
        });
    });

    // Function to get a cookie by name
    function getCookie(name) {
        var value = "; " + document.cookie;
        var parts = value.split("; " + name + "=");
        if (parts.length === 2) return parts.pop().split(";").shift();
    }

    // Scroll to the bottom of the messages div
    function scrollToBottom() {
        const element = document.querySelector('.messages');
        if (element) {
            element.scrollTop = element.scrollHeight;
        }
    }

    // Refresh the messages list
    function refreshMessages(channel) {
        fetch(`/Home/GetMessages?channel=${channel}`)
            .then(response => response.json())
            .then(messages => {
                const messagesDiv = document.querySelector('.messages');
                messagesDiv.innerHTML = ''; // Clear current messages
                messages.forEach(msg => {
                    const msgDiv = document.createElement('div');
                    msgDiv.textContent = `${msg.author}: ${msg.message}`;
                    messagesDiv.appendChild(msgDiv);
                });
                scrollToBottom();
            });
    }

    // Call scrollToBottom when the page loads
    scrollToBottom();

    // SignalR setup
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    connection.on("ReceiveMessage", function (user, message) {
        const msg = document.createElement('div');
        msg.textContent = `${user}: ${message}`;
        document.querySelector('.messages').appendChild(msg);
        scrollToBottom();
    });

    connection.start().then(function () {
        const channel = document.getElementById('Channel').textContent;
        connection.invoke("JoinChannel", channel).catch(function (err) {
            return console.error(err.toString());
        });
    });

    // Join the channel when the page loads
    const channel = document.getElementById('Channel').textContent;
    connection.invoke("JoinChannel", channel).catch(function (err) {
        return console.error(err.toString());
    });

    // Scroll to the bottom of the messages div when the page loads or reloads
    window.addEventListener('load', scrollToBottom);
    window.addEventListener('resize', scrollToBottom);
});
