'use strict';
var _typeof = 'function' == typeof Symbol && 'symbol' == typeof Symbol.iterator ? function(d) {
		return typeof d
	} : function(d) {
		return d && 'function' == typeof Symbol && d.constructor === Symbol && d !== Symbol.prototype ? 'symbol' : typeof d
	},
	_createClass = function() {
		function d(f, g) {
			for (var j, h = 0; h < g.length; h++) j = g[h], j.enumerable = j.enumerable || !1, j.configurable = !0, 'value' in j && (j.writable = !0), Object.defineProperty(f, j.key, j)
		}
		return function(f, g, h) {
			return g && d(f.prototype, g), h && d(f, h), f
		}
	}();

function _classCallCheck(d, f) {
	if (!(d instanceof f)) throw new TypeError('Cannot call a class as a function')
}
var Dialog = function() {
	window.addEventListener('load', function() {
		document.body.addEventListener('touchstart', function() {}, !1)
	}, !1);
	var g = function() {
		function h() {
			_classCallCheck(this, h), this.timer = null, this.set = {}
		}
		return _createClass(h, [{
			key: 'extend',
			value: function extend(j, k) {
				for (var l in k) j[l] = k[l]
			}
		}, {
			key: 'init',
			value: function init(j, k) {
				k && 'object' === ('undefined' == typeof k ? 'undefined' : _typeof(k)) && this.extend(this.set, k);
				var m = document.createElement('div'),
					p = document.createElement('div'),
					q = this,
					r = q.set;
				if (m.classList.add('c_alert_dialog'), r.index && (m.dataset.index = r.index), p.classList.add('c_alert_wrap'), p.innerHTML = '<div class="c_alert_con" style="' + r.style + '">' + j + '</div>', r.addClass && p.classList.add(r.addClass), r.title && (p.classList.add('c_alert_width'), p.insertAdjacentHTML('afterbegin', '<div class="c_alert_title">' + k.title + '</div>')), r.button) {
					p.classList.add('c_alert_width');
					var s = '';
					for (var u in r.button) s += '<a href="javascript:;" data-name="' + u + '">' + u + '</a>';
					p.insertAdjacentHTML('beforeend', '<div class="c_alert_btn">' + s + '</div>');
					var v = p.querySelectorAll('.c_alert_btn a');
					[].forEach.call(v, function(w) {
						w.onclick = function(x) {
							x.preventDefault(), r.button[w.dataset.name].call(p, q)
						}
					})
				}
				r.time && (q.timer = setTimeout(function() {
					_D_obj.close(p, r.after)
				}, r.time + 300)), k && 'object' !== ('undefined' == typeof k ? 'undefined' : _typeof(k)) && (q.timer = setTimeout(function() {
					_D_obj.close(p, r.after)
				}, k + 300)), r.before && r.before.call(p), (void 0 === r.mask || r.mask) && m.insertAdjacentHTML('beforeend', '<div class=\'c_alert_mask\'  ontouchmove=\'return false\'></div>'), m.appendChild(p), document.body.appendChild(m), (void 0 === r.mask || r.mask) && (m.querySelector('.c_alert_mask').onclick = function(w) {
					w.preventDefault(), (r.maskClick || void 0 === r.maskClick) && _D_obj.close(p, r.after)
				}), r.onload && r.onload.call(p), setTimeout(function() {
					m.classList.add('dialog_open')
				}, 50)
			}
		}]), h
	}();
	return window._D_obj = {
		init: function init(h, j, k) {
			new g().init(h, j, k)
		},
		close: function close(h, j) {
			var k = document.querySelectorAll('.c_alert_dialog');
			[].forEach.call(k, function(l) {
				(l.dataset.index == h || l === h.parentNode) && (l.classList.remove('dialog_open'), l.classList.add('dialog_close'), j && j.call(l.querySelector('.c_alert_wrap'), h), l.querySelector('.c_alert_wrap').addEventListener('animationend', function() {
					l.remove()
				}))
			})
		}
	}, _D_obj
}(window, document);