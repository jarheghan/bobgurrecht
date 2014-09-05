/*
 * jQuery File Upload Plugin JS Example 6.5.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
   // $('#fileupload').fileupload();

    //$('#fileupload').fileupload('option', {
    //        maxFileSize: 500000000,
    //        resizeMaxWidth: 1920,
    //        resizeMaxHeight: 1200
    //});

         var el = $('#fileupload'),
        img = el.find('img'),
        elHidden = el.find('.hidden'),
        elRemove = el.find('.remove');

        $('#fileupload1').fileupload({
            url: '/Admin/Picture/AsyncUpload',
            dataType: 'json',
            //acceptFileTypes: /^image\/(gif|jpeg|jpg|png)$/,
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,

            done: function (e, data) {
                debugger;
                var result = data.result;
                if (result.success) {
                    img.attr('src', result.imageUrl);
                    elHidden.val(result.pictureId);
                    elRemove.show();
                }
            },

            error: function (jqXHR, textStatus, errorThrown) {
                if (errorThrown === 'abort') {
                    //alert('File Upload has been canceled');
                }
            }
        });

        elRemove.click(function (e) {
            img.attr('src', '');
            elHidden.val(0);
            $(this).hide();
            e.preventDefault();
        });
});
