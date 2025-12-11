// STRING FUNCTIONS
function trim( str ) {
	// Immediately return if no trimming is needed
	if( (str.charAt(0) != ' ') && (str.charAt(str.length-1) != ' ') ) { return str; }
	// Trim leading spaces
	while( str.charAt(0)  == ' ' ) {
		str = '' + str.substring(1,str.length);
	}
	// Trim trailing spaces
	while( str.charAt(str.length-1)  == ' ' ) {
		str = '' + str.substring(0,str.length-1);
	}

	return str;
}

function strReplace( entry, bad, good ) {
	temp = "" + entry; // temporary holder
	while( temp.indexOf(bad) > -1 ) {
		pos= temp.indexOf( bad );
		temp = "" + ( temp.substring(0, pos) + good +
			temp.substring( (pos + bad.length), temp.length) );
	}
	return temp;
}

function toWebTextInput(entry){
  	string = '' + entry;
  	string = strReplace(string,"&amp;","&");
        string = strReplace(string,"&gt;",">");
        string = strReplace(string,"&lt;","<");
        string = strReplace(string,"&quot;","\"");
        string = strReplace(string,"&#39;","'");
        return string;
}

// ---Format Money
function Comma(number) {
  number = '' + number;
  if (number.length > 3) {
    var mod = number.length % 3;
    var output = (mod > 0 ? (number.substring(0,mod)) : '');
    for (i=0 ; i < Math.floor(number.length / 3); i++) {
      if ((mod == 0) && (i == 0))
        output += number.substring(mod+ 3 * i, mod + 3 * i + 3);
      else
        output+= ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
    }
    return (output);
  }
  else
    return number;
}


function showMoney(money) {
  string = '';
  if (money.substring(0,1)!='0') {
    string += '$';
    string += Comma(money);
  }
  return string;
}


/**
 *  讓function觸發一次後，一定時間內不會再觸發，防止大量觸發
 *
 *  @param func {Function} 要執行的函數
 *  @param limit {Number} 一段時間(毫秒)
 *
 *  @example
 *  function sayHi(userName) { console.log(`hi ${userName}`); }
 *  const sayHiThrottled = throttle((userName) => sayHi(userName), 1000);
 *
 *  // 60次觸發，但只會執行2次
 *  for (let i = 0; i < 60; i++) {
 *    await wait(20);
 *    sayHiThrottled('Alice');
 *  }
 */
function throttle(func, limit) {
  let inThrottle = false;
  return function() {
    const args = arguments;
    const context = this;
    if (!inThrottle) {
      const result = func.apply(context, args);
      inThrottle = true;
      setTimeout(() => inThrottle = false, limit);
      return result;
    }
  }
}

/**
 *  讓function被連續觸發時，只在最後一次觸發後的隔段時間執行，防止大量觸發
 *
 *  @param func {Function} 要執行的函數
 *  @param wait {Number} 一段時間(毫秒)
 *  @param immediate {Boolean} 是否先立即執行一次
 *
 *  @example
 *  function sayHi(userName) { console.log(`hi ${userName}`); }
 *  const sayHiDebounced = debounce((userName) => sayHi(userName), 1000);
 *
 *  // 60次觸發，只會執行1次，在最後一次觸發後的1秒後執行
 *  for (let i = 0; i < 60; i++) {
 *    await wait(20);
 *    sayHiDebounced('Alice');
 *  }
 */
function debounce(func, wait, immediate = false) {
  let timeoutId;
  return function() {
    const context = this;
    const args = arguments;
    const later = function() {
      timeoutId = null;
      if (!immediate) func.apply(context, args);
    };
    const callNow = immediate && !timeoutId;
    clearTimeout(timeoutId);
    timeoutId = setTimeout(later, wait);
    if (callNow) func.apply(context, args);
  };
}


/**
 *  依傳入url開啟新視窗，通常拿來開啟編輯視窗，e.g., 所有權人、法院鑑價共用元件
 */
function tableEdit(url, width = 620, height = 400) {
  // calculate the position of the new window
  // place it around the center of the screen
  const y = window.top.outerHeight / 2 + window.top.screenY - ( height / 2) - 100; // 往上一咪咪
  const x = window.top.outerWidth / 2 + window.top.screenX - ( width / 2);
  wina = window.open(url, "window", `toolbar=no, left=${x}, top=${y}, width=${width}, height=${height}, status=no, scrollbars=yes, resize=yes, menubar=no resizable=1, location=0`);
}

function wait(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

/**
 * 檢查兩個陣列是否相等
 *
 * @param arr1
 * @param arr2
 * @returns {boolean}
 */
function arraysEqual(arr1, arr2) {
    return JSON.stringify(arr1) === JSON.stringify(arr2);
}

/**
 * e.g., numbers.remove(x => x % 2 === 0);
 *
 * @param predicate {Function} - A function that returns a boolean value
 */
Array.prototype.removeAll = function (predicate) {
    let i = this.length;
    while (i--) {
        if (predicate(this[i])) {
            this.splice(i, 1);
        }
    }
}

/**
 * 取得指定名稱的cookie值
 *
 * @param name {string} - Cookie名稱
 * @returns {string|null}
 */
function getCookie(name) {
  const match = document.cookie.match(new RegExp('(?:^|; )' + name + '=([^;]*)'));
  return match ? decodeURIComponent(match[1]) : null;
}

/**
 * 設定一個cookie
 *
 * @param name
 * @param value
 * @param days {number} - Cookie的有效天數，預設為7天
 */
function setCookie(name, value, days= 7, path = '/') {
  const date = new Date();
  date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
  document.cookie = `${name}=${value}; expires=${date.toUTCString()}; path=${path}`;
}
