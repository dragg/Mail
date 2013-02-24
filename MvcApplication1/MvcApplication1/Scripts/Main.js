$(ready);

function ready()
{
    $(".Letter").click(function () {
        requestLetter(this.all.Id.value);
    });

    function requestLetter(idLetter)
    {
        var jqxhr = $.post("/api/Post", idLetter);

        jqxhr.done(function (data) {
        });

        jqxhr.fail(function () {
            //requestLetter();
        });
    }
}