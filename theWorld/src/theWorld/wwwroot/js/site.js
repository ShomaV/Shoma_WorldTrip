(function() {
    //var ele = $("#username");
    //ele.text("Sai Sai");
    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    alert("hello");
    //    main.style = "backgroud-color:blank";
    //});
    //main.on("mouseleave",function() {
    //    main.style = "backgroud-color:white";
    //});

    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function () {
    //    var me = $(this);
    //    alert("hello "+ me.text());
    //});
    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa");
    $("#sidebarToggle").on("click", function () {        
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");            
        }
    });
    
})();

