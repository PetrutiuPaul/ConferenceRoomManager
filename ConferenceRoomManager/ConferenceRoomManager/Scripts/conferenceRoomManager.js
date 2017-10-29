$(document).ready(function (e) {
    $(document).on("click", ".conf1", function (event) {
        $(".conf1").toggleClass("active")
        $(".conf1").toggleClass("not-active")
    })

    function hide() {
        var allRooms = $("td.room-name")
        var notif = $(".notification").parent().css("display", "none")
        //for (var i = 0; i < notif.length; i++) {
        //    console.log($(notif[i]))
        //    $(notif[i]).style("display", "none")
        //}
        for (var i = 0; i < allRooms.length; i++) {
            $("#" + $(allRooms[i]).attr("Id") + ".notification").parent().css("display", "")
        }
    }

    $(document).on("search", "#search", function () {
        console.log($("#available"))
        $.ajax({
            url: $("#getActiveRoomSrc").val(),
            type: "GET",
            data: {
                'search': $("#search").val(),
                'available': $("#available")[0].checked
            },
            success: function (e) {
                $("#rooms").html(e)
            }
        }).done(hide)
    })

    var s = setInterval(function (e) {
        $.ajax({
            url: $("#getActiveRoomSrc").val(),
            type: "GET",
            data: {
                'search': $("#search").val(),
                'available': $("#available")[0].checked
            },
            success: function (e) {
                $("#rooms").html(e)
            }
        }).done(hide)
    }, 5000)

    function toogleNotify() {
        $(this).toggleClass("add-notification")
        $(this).toggleClass("glyphicon-remove")
        $(this).toggleClass("glyphicon-plus")
        $(this).toggleClass("remove-notification")
    }

    $(document).on("click", "#available", function () {
            $.ajax({
                url: $("#getActiveRoomSrc").val(),
                type: "GET",
                data: {
                    'search': $("#search").val(),
                    'available': $(this)[0].checked
                },
                success: function (e) {
                    $("#rooms").html(e)
                }
            }).done(hide)
    })

    $(document).on("click", ".add-notification", toogleNotify)

    $(document).on("click", ".remove-notification", toogleNotify)

    $(document).on("click", ".notify-btn", function () {
        $(".add-notification").trigger("click")
    })

    var inter = setInterval(function (ev) {
        var allActive = $(".remove-notification")
        for (var i = 0; i < allActive.length; i++) {
            if ($($("#" + $(allActive[i]).attr("Id") + ".circle")[0]).hasClass("not-active")) {
                $.notify("Room " + $("td#" + $(allActive[i]).attr("Id")).text() + " is now available.", "info");
                $(allActive[i]).toggleClass("add-notification")
                $(allActive[i]).toggleClass("glyphicon-remove")
                $(allActive[i]).toggleClass("glyphicon-plus")
                $(allActive[i]).toggleClass("remove-notification")
            }
        }
    },3101)
})