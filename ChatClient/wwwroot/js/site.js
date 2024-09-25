// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.channel').click(function (e) {
    e.preventDefault();
    var channelName = $(this).data('channel');
    $.ajax({
        url: '/Channels/'+channelName,
        type: 'GET',
        data: { channelName: channelName },
        success: function (result) {
            $('#channelMessages').html(result);
        }
    });
});

$('.sendMessage').submit(function (e) {
    e.preventDefault();
    var message = $('#Message').val();
    var channel = $('#Channel').text(); // Use .text() to get the channel name from the <h1> element
    var user = $('#User').val();
    user = user === '' ? 'Anonymous' : user;
    $.ajax({
        url: '@Url.Action("SendMessage", "Home")',
        type: 'POST',
        data: { author: user, message: message, channel: channel },
        success: function (result) {
            $('#Message').val('');
            // Optionally, you can refresh the messages list here
        }
    });
});