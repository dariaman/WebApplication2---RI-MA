jqxAlert = {
    // top offset.
    top: 0,
    // left offset.
    left: 0,
    // opacity of the overlay element.
    overlayOpacity: 0.4,
    // background of the overlay element.
    overlayColor: '#ddd',
    // display alert.
    Alert: function (message, title) {
        
        if (title == null) title = 'Alert';
        jqxAlert._show(title, message);
    },
    Information: function (message, title) {
        if (title == null) title = 'Information';
        jqxAlert._show(title, message);
    },
    // initializes a new alert and displays it.
    _show: function (title, msg) {
        jqxAlert._hide();
        jqxAlert._handleOverlay('show');
        
        //if (title == 'Alert') {
        $("BODY").append(
                         '<div class="jqx-alert" style="width: auto; height: auto; overflow: hidden; white-space: nowrap;" id="alert_container">' +
                         '<center><div id="alert_title"></div></center>' +
                         '<div id="alert_content">' +
                         '<table><tr><td style="padding: 5px;"><img alt="' + title + '" src="images/' + title + '.png" /></td> ' +
                         '<td style="padding: 5px;"><div id="message"></div></td></tr></table>' +
                         '<center><input style="margin-bottom: 3px;" type="button" value="Ok" id="alert_button" /></center>' +
                         '</div>' +
                         '</div>');
            $("#alert_title").text(title);
            $("#alert_title").addClass('jqx-alert-header');
            $("#alert_content").addClass('jqx-alert-content-' + title + '');
            $("#message").text(msg);
            $("#alert_button").width(70);
            $("#alert_button").click(function () {
                jqxAlert._hide();
            });
            jqxAlert._setPosition();
        //}
        //if (title == 'Information') {
        //    $("BODY").append(
        //                 '<div class="jqx-alert" style="width: auto; height: auto; overflow: hidden; white-space: nowrap;" id="alert_container">' +
        //                 '<center><div id="alert_title"></div></center>' +
        //                 '<div id="alert_content">' +
        //                 '<table><tr><td><img alt="Information" src="images/Information.png" /></td> ' +
        //                 '<td><div id="message"></div></td></tr></table>' +
        //                 '<center><input style="margin-top: 15px;" type="button" value="Ok" id="alert_button"/></center>' +
        //                 '</div>' +
        //                 '</div>');
        //    $("#alert_title").text(title);
        //    $("#alert_title").addClass('jqx-alert-header');
        //    $("#alert_content").addClass('jqx-alert-content-Information');
        //    $("#message").text(msg);
        //    $("#alert_button").width(70);
        //    $("#alert_button").click(function () {
        //        jqxAlert._hide();
        //    });
        //    jqxAlert._setPosition();
        //}
    },
    // hide alert.
    _hide: function () {
        $("#alert_container").remove();
        jqxAlert._handleOverlay('hide');
    },
    // initialize the overlay element.
    _handleOverlay: function (status) {
        switch (status) {
            case 'show':
                jqxAlert._handleOverlay('hide');
                $("BODY").append('<div id="alert_overlay"></div>');
                $("#alert_overlay").css({
                    position: 'absolute',
                    zIndex: 99998,
                    top: '0px',
                    left: '0px',
                    width: '100%',
                    height: $(document).height(),
                    background: jqxAlert.overlayColor,
                    opacity: jqxAlert.overlayOpacity
                });
                break;
            case 'hide':
                $("#alert_overlay").remove();
                break;
        }
    },
    // sets the alert's position.
    _setPosition: function () {
        // center screen with offset.
        var top = (($(window).height() / 2) - ($("#alert_container").outerHeight() / 2)) + jqxAlert.top;
        var left = (($(window).width() / 2) - ($("#alert_container").outerWidth() / 2)) + jqxAlert.left;
        if (top < 0) {
            top = 0;
        }
        if (left < 0) {
            left = 0;
        }
        // set position.
        $("#alert_container").css({
            top: top + 'px',
            left: left + 'px'
        });
        // update overlay.
        $("#alert_overlay").height($(document).height());
    }
}