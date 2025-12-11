api.exportExcel = async function ({url, params, filename = 'export.xlsx'}) {
  top.showLoader && top.showLoader();

  try {
    const response = await api.post(url, params, {
      responseType: 'blob'
    });
    const blob = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    const urlBlob = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = urlBlob;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  } catch (error) {
    console.error(error);
    throw error;
  }  finally {
    top.hideLoader && top.hideLoader();
  }
}
