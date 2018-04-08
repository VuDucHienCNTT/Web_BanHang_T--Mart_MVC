/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.language = 'vi';
    config.toolbar = [
                                   ['Source', 'Preview', 'Templates'],
                                   ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
                                   ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
                                   '/',
                                   ['Bold', 'Italic', 'Underline', 'Strike', '-'],
                                   ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
                                   ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                                   ['BidiLtr', 'BidiRtl'],
                                   ['Link', 'Unlink', 'Anchor'],
                                   ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'],
                                   '/',
                                   ['Styles', 'Format', 'Font', 'FontSize'],
                                   ['TextColor', 'BGColor'],
                                   ['Maximize', 'ShowBlocks', 'Syntaxhighlight']
    ]

    config.filebrowserBrowseUrl = "/Assets/Admin/ckfinder/ckfinder.html";
    config.filebrowserImageUrl = "/Assets/Admin/ckfinder/ckfinder.html?type=Images";
    config.filebrowserFlashUrl = "/Assets/Admin/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserUploadUrl = "/Assets/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowerImageUploadUrl = "/Assets/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/Assets/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
};
