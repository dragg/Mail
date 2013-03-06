$(ready);

function ready()
{
    $(".Letter").click(function () {
        requestLetter(this.all.Id.value, this);
    });
    
    $(".write").click(function()
    {
        Write();
    });

    $('.inbox').click(function () {
        box('inbox');
    });

    $('.outbox').click(function () {
        box('outbox');
    });

    function box(par)
    {
        var jqxhr = $.getJSON("/api/letter?valu=" + par + "&cookie=" + document.cookie);
        jqxhr.done(function (data) {
            $("#Letters").remove();
            $(".Write").remove();
            var e = $('<div>', { "id": "Letters" });
            for (var i = 0; i < data.length; i++) {
                var item = $('<div>', { "class": "Letter" });
                var dat = 
                        '<div class="Whom">'+
                            (par == "inbox" ? data[i].UserFromWhom : data[i].UserWhom) +
                        '</div>'+
                        '<div class="Subject">'+
                            '<a class="getLetter">'+data[i].Subject+'</a>'+
                        '</div>'+
                        '<input type="hidden" value='+data[i].Id+' id="Id" name="Id" />';
                        e.append(item.html(dat));
            }
            e.appendTo("#message");

            $(".Letter").click(function () {
                requestLetter(this.all.Id.value, this);
            });
        });
    }

    function Write()
    {
        var jqxhr = $.post("/api/letter?val=" + "write", {});

        jqxhr.done(function () {
            $("#Letters").remove();
            $(".Write").remove();
            var e = $('<div>', { "class": "Write" });
            var data =
            '<form action="/Home/To_Write" method="post">' +
                '<p>' +
                    '<text>Кому:</text>' +
                    '<input type="text" name="Whom" />' +
                '</p>' +
                '<p>' +
                    '<text>Тема:</text>' +
                    '<input type="text" name="Subject" />' +
                '</p>' +
                '<p>' +
                    '<text>Текст письма:</text>' +
                '</p>' +
                '<textarea name="Text"></textarea>' +
                '<input id="button" type="submit" value="To Send" />' +
            '</form>';
            e.append($("<div>").html(data));
            e.appendTo($("#message"));
        });
    }

    function requestLetter(idLetter, where)
    {
        var jqxhr = $.post("/api/letter?value=" + idLetter, {});

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