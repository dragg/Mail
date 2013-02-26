$(ready);

function ready()
{
    $(".Letter").click(function () {
        requestLetter(this.all.Id.value, this);
    });

    function requestLetter(idLetter, where)
    {
        var jqxhr = $.post("/api/letter?value=" + idLetter, { value: idLetter });

        jqxhr.done(function (data) {
            $('.TextLetter').remove();
            var e = $('<div>', { "class": "TextLetter" });
            for (var i = 0; i < 100; i++) {
                data = data.replace("\n", "<br/>");
            }
            e.append($("<div>").html(data));
            e.appendTo($(where));
        });

        jqxhr.fail(function () {
            requestLetter(idLetter, where);
        });
    }
}