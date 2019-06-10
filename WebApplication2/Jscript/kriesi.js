function mainmenu(){
    $("#navigationMenumenu ul ").css({ display: "none" }); // Opera Fix
    $("#navigationMenumenu li ").hover(function () {
		$(this).find('ul:first').css({visibility: "visible",display: "none"}).show(0);
		},function(){
		$(this).find('ul:first').css({visibility: "hidden"});
		});

    $("#navigationMenusetting ul ").css({ display: "none" }); // Opera Fix
    $("#navigationMenusetting li ").hover(function () {
        $(this).find('ul:first').css({ visibility: "visible", display: "none" }).show(0);
    }, function () {
        $(this).find('ul:first').css({ visibility: "hidden" });
    });
}
  
 $(document).ready(function(){					
	mainmenu();
});