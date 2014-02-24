/* ===========================================================
 * bootstrap-popover.js v2.0.1
 * http://twitter.github.com/bootstrap/javascript.html#popovers
 * -----------------------------------------------------------
 * MOD BY GDUPUY TO INCLUDE CUSTOM CLASSES + DESTROY METHOD
 * ONLY CONSISTS IN RELYING ON CLASSYPOPOVER RATHER THAN POPOVER
 * + ADDING DESTROY METHOD, SEE l.74
 * ===========================================================
 * Copyright 2012 Twitter, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * =========================================================== */


!function( $ ) {

 "use strict"

  var ClassyPopover = function ( element, options ) {
    this.init('classyPopover', element, options)
  }

  /* NOTE: CLASSYPOPOVER EXTENDS BOOTSTRAP-CLASSYTOOLTIP.js
     ========================================== */

  ClassyPopover.prototype = $.extend({}, $.fn.classyTooltip.Constructor.prototype, {

    constructor: ClassyPopover

  , setContent: function () {
      var $tip = this.tip()
        , title = this.getTitle()
        , content = this.getContent()

      $tip.find('.popover-title')[ $.type(title) == 'object' ? 'append' : 'html' ](title)
      $tip.find('.popover-content > *')[ $.type(content) == 'object' ? 'append' : 'html' ](content)

      $tip.removeClass('fade top bottom left right in')
    }

  , hasContent: function () {
      return this.getTitle() || this.getContent()
    }

  , getContent: function () {
      var content
        , $e = this.$element
        , o = this.options

      content = $e.attr('data-content')
        || (typeof o.content == 'function' ? o.content.call($e[0]) :  o.content)

      content = content.toString().replace(/(^\s*|\s*$)/, "")

      return content
    }

  , tip: function() {
      if (!this.$tip) {
        this.$tip = $(this.options.template)
      }
      return this.$tip
    }
  , destroy: function () {
      this.$element.off().removeData('classyPopover')
    }

  })


 /* CLASSYPOPOVER PLUGIN DEFINITION
  * ======================= */

  $.fn.classyPopover = function ( option ) {
    return this.each(function () {
      var $this = $(this)
        , data = $this.data('classyPopover')
        , options = typeof option == 'object' && option
      if (!data) $this.data('classyPopover', (data = new ClassyPopover(this, options)))
      if (typeof option == 'string') data[option]()
    })
  }

  $.fn.classyPopover.Constructor = ClassyPopover

  $.fn.classyPopover.defaults = $.extend({} , $.fn.classyTooltip.defaults, {
    placement: 'right'
  , content: ''
  , template: '<div class="popover"><div class="arrow"></div><div class="popover-inner"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>'
  })

}( window.jQuery );