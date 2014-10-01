// <reference path="../jquery-2.1.1.intellisense.js" />
// <reference path="../jquery.validate.min.js" />

var catalog = (function () {
 $('#productvariation').dialog({
        title: "Production Variation",
        autoOpen: false,
        buttons: [
            {
                text: "Save"
              , 'class': "btn-primary"
              , click: function () {
                  $(this).dialog("close");
              }
            },

           {
               text: "Cancel"
              , 'class': "btn-warning"
              , click: function () {
                  $(this).dialog("close");
              }
           }
        ]
    });

    $('#btnProdVariation').click(function () {
        $('#productvariation').dialog('open').load('/Admin/Common/ProductVariation');
    })

   

   
})();