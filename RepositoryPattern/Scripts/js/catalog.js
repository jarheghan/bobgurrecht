// <reference path="../jquery-2.1.1.intellisense.js" />
// <reference path="../jquery.validate.min.js" />

var catalog = (function () {
    var i1 = 0;
    var cnt = parseInt($('body').find('#pvCount').val());

    $("td.mmm1").dblclick(function () {
        var $ss = $(this);
        var i = $ss.data("id");
        var dis = $ss.data("des")
        var pvid = $('body').find('#ProductVariation_' + i + '__ID').val();
        var OriginalContent = $ss.text();

        $ss.addClass("cellEditing");
        $ss.html("<input type='text' value='" + OriginalContent + "' />");
        $ss.children().first().focus();
        $ss.children().first().keypress(function (e) {
            if (e.which === 13) {
                var newContent = $(this).val();
                $(this).parent().text(newContent);
                $(this).parent().removeClass("cellEditing");
                $ss.append('<input type="hidden" name="ProductVariation[' + i + '].' + dis + '" value="' + newContent + '" />');
                $ss.append('<input type="hidden" name="ProductVariation[' + i + '].ID value="' + pvid + '" id="ProductVariation_' + i + '__ID" />');
            }
        });
       

        $ss.children().first().blur(function () {
            $(this).parent().text(OriginalContent);
            $(this).parent().removeClass("cellEditing");
        });
    });


 $('#productvariation').dialog({
        title: "Production Variation",
        autoOpen: false,
        buttons: [
            {
                text: "Save"
              , 'class': "btn-primary"
              , click: function () {
                  var prodTable = $("#tablePrdVariation");
                  var des = $('body').find('#description').val();
                  var size = $('body').find('#size').val();
                  prodTable.append('<tr id="tr' + i1 + '"><td><input type="hidden" name="[' + i1 + '].Description" value="' + des + '"/>' + des + '</td><td><input type="hidden" name="[' + i1 + '].Size" value="' + size + '"/>' + size
                      + '</td><td><a href="#" onclick="catalog.removeVariation(' + i1 + ')">Remove</a></td></tr>');
                  i1++;
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

 $('#productvariationedit').dialog({
     title: "Production Variation",
     autoOpen: false,
     buttons: [
         {
             text: "Save"
           , 'class': "btn-primary"
           , click: function () {
               var prodTable = $("#tablePrdVariation");
               var des = $('body').find('#description').val();
               var size = $('body').find('#size').val();
               debugger;
               cnt = cnt++;
               prodTable.append('<tr id="tr' + cnt + '"><td><input type="hidden" name="ProductVariation[' + cnt + '].Description" value="' + des + '"/>' + des + '</td><td><input type="hidden" name="ProductVariation[' + cnt + '].Size" value="' + size + '"/>' + size
                   + '</td><td><a href="#" onclick="catalog.removeVariation(' + cnt + ')">Remove</a></td><td><a href="#" onclick="catalog.editVariation(' + cnt + ')">Edit</a></td></tr>');
               
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

 var dialogEditProductVariation = $('#productvariationedit').dialog({
                                 title: "Production Variation",
                                 autoOpen: false,
                                 buttons: [
                                     {
                                         text: "Save"
                                       , 'class': "btn-primary"
                                       , click: function () {
                                           var prodTable = $("#tablePrdVariation");
                                           var des = $('body').find('#description').val();
                                           var size = $('body').find('#size').val();

                                           prodTable.append('<tr id="tr' + cnt + '"><td><input type="hidden" name="ProductVariation[' + cnt + '].Description" value="' + des + '"/>' + des + '</td><td><input type="hidden" name="ProductVariation[' + cnt + '].Size" value="' + size + '"/>' + size
                                               + '</td><td><a href="#" onclick="catalog.removeVariation(' + cnt + ')">Remove</a></td><td><a href="#" onclick="catalog.editVariation(' + cnt + ')">Edit</a></td></tr>');
                                           cnt++;
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
    $('#btnProdVariationedit').click(function () {
        $('#productvariationedit').dialog('open').load('/Admin/Common/ProductVariation');
    })

    var messagealert = function (msg) {
        $.messager.alert(msg);
    }

    var prodImage = function () {
        $("div#dropzoneForm").dropzone({
            url: "/Admin/Picture/SaveUploadedFile",
            maxFiles: 2,
            init: function () {
                this.on("maxfilesexceeded", function (data) {
                    var res = eval('(' + data.xhr.responseText + ')');

                });
                this.on("success", function (file, response) {
                    var addHiddenValue = Dropzone.createElement('<input type="hidden" name="PictureID" id="pic_idx" value="' + response.PictureId + '"/>');
                    file.previewElement.appendChild(addHiddenValue);
                });
                this.on("addedfile", function (file) {

                    // Create the remove button
                    var removeButton = Dropzone.createElement('<a href="#" class="btn btn-primary">Remove file</a>');


                    // Capture the Dropzone instance as closure.
                    var _this = this;

                    // Listen to the click event
                    removeButton.addEventListener("click", function (e) {
                        // Make sure the button click doesn't submit the form:
                        var j = $('#pic_idx').val();
                        $.ajax({
                            url: '/Admin/Picture/RemoveUploadedFile?Id=' + j,
                        }).done(function (data) {
                            if (data.Message === 'Success') {
                                e.preventDefault();
                                e.stopPropagation();
                                // Remove the file preview.
                                _this.removeFile(file);
                                // If you want to the delete the file on the server as well,
                                // you can do the AJAX request here.
                            }
                        });

                    });

                    // Add the button to the file preview element.
                    file.previewElement.appendChild(removeButton);
                });
            }
        });
    }

    $('#btnSignIn').click(function () {
        $('#test1').dialog({
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
        })

        $('#test1').dialog('open');
    });
   
    var editProductImage = function () {
        $("div#mydropzone").dropzone({
            url: "/Admin/Picture/SaveUploadedFile",
            maxFiles: 1,
            init: function () {
                var mm = $('#mypic_id').val();
                var thiszone = this;
                var mockFile = { name: "", size: 0 };
                var removeButton = Dropzone.createElement('<a href="#" class="btn btn-primary">Remove file</a>');
                $.getJSON('/Admin/Picture/GetPicture?Id=' + mm, function (data) {
                    var addHiddenValue = Dropzone.createElement('<input type="hidden" name="Product.PictureID" id="pic_idx" value="' + mm + '"/>');
                    mockFile.name = data.FilePath
                    mockFile.size = 12345;
                    thiszone.emit("addedfile", mockFile)
                    thiszone.emit("thumbnail", mockFile, "/Images/WallImages/imagepath/" + mockFile.name);
                    mockFile.previewElement.appendChild(removeButton);
                    mockFile.previewElement.appendChild(addHiddenValue);
                })

                this.on("success", function (file, response) {
                    var addHiddenValue = Dropzone.createElement('<input type="hidden" name="Product.PictureID" id="pic_idx" value="' + response.PictureId + '"/>');
                    file.previewElement.appendChild(addHiddenValue);
                });

                removeButton.addEventListener("click", function (e) {
                    var j = $('#pic_idx').val();
                    $.ajax({
                        url: '/Admin/Picture/RemoveUploadedFile?Id=' + j,
                    }).done(function (data) {
                        if (data.Message === 'Success') {
                            e.preventDefault();
                            e.stopPropagation();
                            thiszone.removeFile(mockFile);
                        }
                    });

                });


                thiszone.on("addedfile", function (file) {
                    //Dropzone.options.url="/Admin/Picture/SaveUploadedFile";
                    file.previewElement.appendChild(removeButton);

                    removeButton.addEventListener("click", function (e) {
                        var j = $('#pic_idx').val();
                        $.ajax({
                            url: '/Admin/Picture/RemoveUploadedFile?Id=' + j,
                        }).done(function (data) {
                            if (data.Message === 'Success') {
                                e.preventDefault();
                                e.stopPropagation();
                                thiszone.removeFile(file);
                            }
                        });

                    });
                });


            }
        });
    }

   

    var removeVariation = function (i) {
        $('#tr' + i).remove();
    }

    var editProdtionVartion = function (id) {
        dialogEditProductVariation.dialog('open').load('/Admin/Common/ProductVariationEdit?id=' + id);
    }
     
  


    return {
        prodImage: prodImage,
        editProductImage: editProductImage,
        removeVariation: removeVariation,
        messagealert: messagealert,
        editProdtionVartion: editProdtionVartion
    }

   
})();