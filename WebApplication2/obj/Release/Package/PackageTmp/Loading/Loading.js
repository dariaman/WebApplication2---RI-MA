function ShowProgress() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
        $(".loading").fadeOut(2000);
        $(".modal").fadeOut(2000);
    }, 0);
}


$(window).load(function () {
    var modal = $('<div />');
    modal.addClass("modal");
    $('body').append(modal);
    var loading = $(".loading");
    loading.show();
    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
    loading.css({ top: top, left: left });
    var loading = $(".loading");
    $(".loading").fadeOut(1000);
    $(".modal").fadeOut(1000);

});

$('[id*=btnSubmit]').live("click", function () {
    ShowProgress();
});

$('[id*=LinkSubmit]').live("click", function () {
    ShowProgress();
});

$('[id*=btnLogin]').live("click", function () {
    ShowProgress();
});


$('[id*=DDLProduct]').live("change", function () {
    ShowProgress();
});

$('[id*=btnAdd]').live("click", function () {
    ShowProgress();
});

$('[id*=btnSearch1]').live("change", function () {
    ShowProgress();
});

$('[id*=btnSearch]').live("change", function () {
    ShowProgress();
});
$(document).ready(function () {
    $('[id*=ddlRoleAP]').on('change', function (e) { ShowProgress(); })
    $('[id*=ddlKeywordAP]').on('change', function (e) { ShowProgress(); })
    $('[id*=DDLBranch]').on('change', function (e) { ShowProgress(); })
    $('[id*=DDLlob]').on('change', function (e) { ShowProgress(); })

});