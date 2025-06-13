(function($){
  $.fn.imageBox = function(options){
    var options = $.extend({
      rotateDirection: 'right'
    }, options);   
    var obj = this, fileName = options.fileName;

    initHtml(obj);
    initCss(obj);

    

    btnCtrlImgEvent(options);
  };

  var rotateDeg = 0;

  function initHtml(obj){
    var div = $('<div id="unbind-pos" class="modal fade" style="display:none;" aria-hidden="true"></div>'); 
    div.append('<div class="modal-dialog">' +
                  '<div class="modal-content">'+
                        '<div class="modal-header">'+
                            '<button aria-hidden="true" data-dismiss="modal" class="close" type="button"><span>&times;</span></button>'+
                            '<h4 class="modal-title">Image preview</h4>'+
                        '</div>'+
                        '<div style="min-height: 350px;max-height: 500px;" class="modal-body">'+
        '<div id="img-preview"><img src="" width="500px" height="350px" class="image-box" style="cursor: move;"></img></div>'+
                            '<div class="img-op">'+
                                '<span class="btn btn-primary zoom-in">Zoom In</span>'+
                                '<span class="btn btn-primary zoom-out">Zoom Out</span>'+
                                '<span class="btn btn-primary rotate">Rotate</span>' +
                                '<span class="btn btn-primary print">Print</span>' +
                            '</div>'+
                        '</div>'+
                        '<div class="modal-footer">'+
                            '<button data-dismiss="modal" class="btn btn-default" type="button">Close</button>'+
                        '</div>'+
                  '</div>'+
                '</div>');
    $(obj).append(div);
  };

  function initCss(obj){
    $(obj).find('#img-preview').css({
      'height': '350px',
      'width': 'auto',
      'overflow': 'auto',
      'text-align': 'center'
    });
    $(obj).find('.img-op').css({
      'margin-top': '5px',
      'text-align': 'center'
    });
    $(obj).find('.modal .modal-content .btn').css('border-radius', '0');
    $(obj).find('.img-op .btn').css({
      'margin': '5px',
      'width': '100px',
    });
    $(obj).find('.modal-footer .btn-default').css({
      'background-color': '#fff',
      'background-image': 'none',
      'border': '1px solid #ccc',
    });
  };

  function btnCtrlImgEvent(options){
    zoomIn();
    zoomOut();
    dragImage();
      rotateImage(options);
      print();
  };

  function zoomIn(){
    $('.zoom-in').click(function(){
      var imageHeight = $('#img-preview img').height();
      var imageWidth = $('#img-preview img').width();
      $('#img-preview img').css({
        height: '+=' + imageHeight * 0.1,
        width: '+=' + imageWidth * 0.1
      });
    });
  };

  function zoomOut(){
    $('.zoom-out').click(function(){
      var imageHeight = $('#img-preview img').height();
      var imageWidth = $('#img-preview img').width();
      $('#img-preview img').css({
        height: '-=' + imageHeight * 0.1,
        width: '-=' + imageWidth * 0.1
      });
    });
  };

  function dragImage(){
    $('#img-preview').on('mousedown', 'img', function(event) {
      var mousePos = { x: event.clientX, y: event.clientY };
      var _this = this;

      var scrollLeft = $(_this).parent().scrollLeft();
      var scrollTop = $(_this).parent().scrollTop();

      $(document).on('mousemove', function(event){
        var offsetX = event.clientX - mousePos.x;
        var offsetY = event.clientY - mousePos.y;

        $(_this).parent().scrollLeft(scrollLeft - offsetX);
        $(_this).parent().scrollTop(scrollTop - offsetY);
      });

      $(document).on('mouseup', function(){
        $(document).off("mousemove");     
      });
      return false;
    });
  };

  function rotateImage(options){
    $('.rotate').click(function() {
      if(options.rotateDirection == 'right'){
        rotateDeg += 90;
        if(rotateDeg == 360){
          rotateDeg = 0;
        }
      }
      if(options.rotateDirection == 'left'){
        rotateDeg -= 90;
        if(rotateDeg == -360){
          rotateDeg = 0;
        }
      }
      $('#img-preview img').css({
        'transform': 'rotate('+ rotateDeg +'deg)',
        '-webkit-transform': 'rotate('+ rotateDeg +'deg)',
        '-moz-transform':'rotate('+ rotateDeg +'deg)',
        '-o-transform': 'rotate('+ rotateDeg +'deg)',
        '-ms-transform': 'rotate('+ rotateDeg +'deg)'
      });
    });
    };

    function print() {

        $('.print').click(function () {
            var divContents = $("#img-preview").html();
            var printWindow = window.open('', '', 'height=600,width=600 padding-top=60');
            printWindow.document.write('<html><head><title>Image Preview</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        });
    }
})(jQuery);
