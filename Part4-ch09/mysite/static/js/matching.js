var TempArr = [];
function bodyevent(a, b, c) {
    $(a + " option").each(function(index, el) {
        TempArr[index] = $(this).text()
    });
    $(document).bind('click',
    function(e) {
        var e = e || window.event;
        var elem = e.target || e.srcElement;
        while (elem) {
            if (elem.id && (elem.id == a.replace('#', '') || elem.id == b.replace('#', '') || elem.id == c.replace('#', ''))) {
                return
            }
            elem = elem.parentNode
        }
        $(a).css({
            "display": "none",
            "border-color": "#b8b8b8"
        });
        $(b).css({
            "border-color": "#b8b8b8"
        });
        $(c + " span").removeClass("up")
    })
};
function changeF(this_, a, b, c) {
    $(this_).prevAll("input[id='" + b + "']").val($(this_).find("option:selected").text());
    $(a).css({
        "display": "none"
    });
    $(c + " span").removeClass("up")
};
function setfocus(this_, a, b, c) {
    $(a).css({
        "display": "",
        "border-color": "#38f"
    });
    $(b).css({
        "border-color": "#38f"
    });
    $(c + " span").addClass("up");
    var select = $(a);
    select.html("");
    for (i = 0; i < TempArr.length; i++) {
        var option = $("<option></option>").text(TempArr[i]);
        select.append(option)
    }
};
function setinput(this_, a) {
    var select = $(a);
    select.html("");
    for (i = 0; i < TempArr.length; i++) {
        if (TempArr[i].indexOf(this_.value) >= 0) {
            var option = $("<option></option>").text(TempArr[i]);
            select.append(option)
        }
    }
};