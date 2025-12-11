const api = axios.create({
  baseURL: ''
});

api.interceptors.request.use(config => {
  if (config.method === 'get' && config.url?.includes('/getCityList') && config.params) {
    config.params = convertNullToEmpty(config.params);
  }
  return config;
});

api.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    if (error.response && error.response.status === 401) {
      location.href = '';
      return new Promise(() => {});
    }

    if (error.response.data instanceof Blob) {
      const blobText = await error.response.data.text();
      const jsonData = JSON.parse(blobText);
      error.response.data = jsonData;
    }

    let errMsg = getErrorMsg(error);
    alert(errMsg);

    return Promise.reject(error);
  }
);

window.api = api;

function getErrorMsg(error) {
  let errMsg = '';

  if (error.response && error.response.data && error.response.data.requestId) {
    errMsg += '作業失敗\n';
    errMsg += `url： ${error.response.config.url}\n`;
    errMsg += `requestId： ${error.response.data.requestId}\n`;
    errMsg += `timestamp： ${error.response.data.timestamp}\n`;
    errMsg += `errorMessage： ${error.response.data.errorMessage}`;
  } else if (error.response && error.response.data) {
    // 後端Request物件，@Validate失敗時走這
    errMsg += `作業失敗：${error.response.data}`;
  }

  error.response.data

  return errMsg;
}

function convertNullToEmpty(params) {
  return Object.fromEntries(
    Object.entries(params).map(([k, v]) => [k, v == null ? '' : v])
  );
}

