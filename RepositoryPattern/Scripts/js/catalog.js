﻿// <reference path="../jquery-2.1.1.intellisense.js" />
// <reference path="../jquery.validate.min.js" />

var catalog = (function () {
    var i1 = 0;
    var cnt = parseInt($('body').find('#pvCount').val());

  

    
   

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

 var dialogEditProductVariation1 = $('#productvariationedit1').dialog({
     title: "Production Variation",
     autoOpen: false,
     buttons: [
         {
             text: "Insert"
           , 'class': "btn-primary"
           , click: function () {
               var mycnt;
               var prodTable = $("#tablePrdVariation");
               prodTable.find('td').each(function (index) {
                   mycnt = index;
               })
               var des = $('body').find('#description').val();
               var size = $('body').find('#size').val();
               var data = {
                   Description: $('body').find('#description').val(),
                   Size: $('body').find('#size').val(),
                   ProductID: $('body').find('#pvProductID').val(),
               }
               $.ajax({
                   url: "/Admin/Common/ProductVariationInsert",
                   type: "POST",
                   datatype: 'json',
                   contentType: 'application/json',
                   data: JSON.stringify(data),
                   cache: false,
                   async: false,
                   success: function (val1) {
                       if (val1.Message === "success") {
                           window.location.href = "/Admin/Product/Edit/" + val1.OutputID;
                           $.messager.alert("hello");
                       }
                       if (val1.Message === "error") {
                           $.messager.alert("Product Variation was not updated. Please call Technical Administrator.");
                       }
                   }

               })
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
                                           var data = {
                                               Description: $('body').find('#descriptionedit').val(),
                                               Size: $('body').find('#sizeedit').val(),
                                               ID: $('body').find('#prdVariationIDedit').val(),
                                           }
                                           $.ajax({
                                               url: "/Admin/Common/ProductVariationUpdate",
                                               type: "POST",
                                               datatype: 'json',
                                               contentType: 'application/json',
                                               data: JSON.stringify(data),
                                               cache: false,
                                               async: false,
                                               success: function (val1) {
                                                   if (val1.Message === "success") {
                                                       window.location.href = "/Admin/Product/Edit/" + val1.OutputID;
                                                       $.messager.alert("hello");
                                                   }
                                                   if (val1.Message === "error") {
                                                       $.messager.alert("Product Variation was not updated. Please call Technical Administrator.");
                                                   }
                                               }

                                           })
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


 dialogauthentication = $('#authentication').dialog({
     title: "Register Customer/Sign In",
     autoOpen: false,
     buttons: [
         {
             text: "Register"
           , 'class': "btn-primary"
           , click: function () {
               // $(this).dialog("close");
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

    $('#btnProdVariation').click(function () {
        $('#productvariation').dialog('open').load('/Admin/Common/ProductVariation');
    })
    $('#btnProdVariationedit').click(function () {
        dialogEditProductVariation1.dialog('open').load('/Admin/Common/ProductVariation');
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
            title: "Sign In",
            autoOpen: false,
        })
        $('#test1').dialog('open').load('/Account/SignInModal');
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
    };

    //var tableedit = function () {
    //    $('#tblProductVariation').editableTableWidget({addProperties:'mmm'});
    //}


    var removeVariation = function (i) {
        $('#tr' + i).remove();
    }

    var editProdtionVartion = function (id) {
        dialogEditProductVariation.dialog('open').load('/Admin/Common/ProductVariationEdit?id=' + id);
    }
   
    var addWishList = function (prodID) {
        var wishList = {};
        wishList.ProductVariationID = $('#ddlProductVariation').val();
        wishList.ProductID = prodID;
        wishList.Quantity = $('#txtQuantity').val();

        $.ajax({
            url: "/Order/AddToWishList",
            type: "POST",
            datatype: 'json',
            contentType: 'application/json',
            data: JSON.stringify(wishList),
            cache: false,
            async: false,
            success: function (val) {
                if (val.Message === "success") {
                    $.messager.alert('<div><span class="alert alert-success">Item has been added to the WishList</span></div>');
                   
                }
                if (val.Message === "error") {
                    $.messager.alert("Product Variation was not updated. Please call Technical Administrator.");
                }
                if (val.Message === "Duplicate") {
                    $.messager.alert("<b>The Product and the Product Variation already exist in the WishList!</b>");
                }

                if (val.Message === "authenticate") {
                    dialogauthentication.dialog('open').load('/Account/CustomerRegistration');
                }
                $('#wish-btn').empty();
                $('#wish-btn').html(val)
            }

        })
    }

    var LoginModal = function () {
        //btnLoginModal
        var data = {
            Username: $('#username').val(),
            Password: $('#password').val()
        }
        $.ajax({
            url: "/Account/LoginModal",
            type: "POST",
            datatype: 'json',
            contentType: 'application/json',
            data: JSON.stringify(data),
            cache: false,
            async: false,
            success: function (val) {
                debugger;
                if (val.Type === "2") {
                    var $error = $('body').find('#divErrorMessage');
                    $error.empty();
                    $error.append('<div class="alert alert-warning" role="alert">' + val.Message + '</div>');
                }
                if (val.Type === "1") {
                    window.location.href = val.Message;
                }
            }
        });
    }

    var validation = function () {
        $('#containerForm').bootstrapValidator({
            container: 'tooltip',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                username: {
                    validators: {
                        notEmpty: {
                            message: 'The usernamee is required'
                        }
                    }
                },
                password: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
                confirmPassword: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
                firstname: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
                lastname: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
                address1: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
                address2: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
                city: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
                zipcode: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
                phone: {
                    validators: {
                        notEmpty: {
                            message: 'The password is required'
                        }
                    }
                },
            }
        });
    }
   


    return {
        prodImage: prodImage,
        editProductImage: editProductImage,
        removeVariation: removeVariation,
        messagealert: messagealert,
        editProdtionVartion: editProdtionVartion,
        addWishList: addWishList,
        LoginModal: LoginModal,
        validation: validation
    }

   
})();