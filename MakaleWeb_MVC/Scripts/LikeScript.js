
$(function () {

    var makaledizi = [];

    $("div[data-makaleid]").each(function(i,e) {
        makaledizi.push($(e).data("makaleid"));
    });

    //1,5,9,12,15,35

    $.ajax({
        method: "POST",
        url: "/Makale/MakaleGetir",
        data: { mid: makaledizi }
    }).done(function (sonuc) {
        if (sonuc.liste != null && sonuc.liste.length>0)
        {
            for (var i = 0; i < sonuc.liste.length; i++)
            {
                var id = sonuc.liste[i];
                var btn = $("button[data-mid=" + id + "]");
                btn.data("like", true);
                var span = btn.find("span.like-kalp-"+id);
                span.removeClass("glyphicon-heart-empty");
                span.addClass("glyphicon-heart");
            }
        }

    }).fail(function () {
        alert("Sunucu ile bağlantı kurulamadı");
    });

    $("button[data-like]").click(function () {

        var btn = $(this);
        var like = btn.data("like");
        var mid = btn.data("mid");
        var spanlike = $("span.like-kalp-"+mid);
        var likecount = $("span.like-"+mid);

        $.ajax({
            method: "POST",
            url: "/Makale/MakaleBegen",
            data: { makaleid: mid, begeni: !like }
        }).done(function (sonuc) {
            if (sonuc.hata) {
                alert("Beğeni işlemi gerçekleştirilemedi");
            }
            else
            {
                var begeni = !like;
                btn.data("like", !like);     

                likecount.text(sonuc.begenisayisi);

                spanlike.removeClass("glyphicon-heart-empty");
                spanlike.removeClass("glyphicon-heart");
                if (begeni)
                {
                    spanlike.addClass("glyphicon-heart");
                }
                else
                {
                    spanlike.addClass("glyphicon-heart-empty");
                }

            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı");
        });


    });


});