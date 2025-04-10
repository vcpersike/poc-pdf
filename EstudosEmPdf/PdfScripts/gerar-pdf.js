const puppeteer = require('puppeteer');
const fs = require('fs');
const path = require('path');

const htmlPath = path.join(__dirname, '../wwwroot/output/entrada.html');
const pdfPath = path.join(__dirname, '../wwwroot/output/saida.pdf');
const outputDir = path.dirname(pdfPath);

if (!fs.existsSync(outputDir)) {
    fs.mkdirSync(outputDir, { recursive: true });
}

(async () => {
    const browser = await puppeteer.launch({
        headless: 'new',
        args: ['--no-sandbox']
    });

    const page = await browser.newPage();
    const html = fs.readFileSync(htmlPath, 'utf8');

    await page.setContent(html, { waitUntil: 'networkidle0' });

    await page.pdf({
        path: pdfPath,
        format: 'A4',
        printBackground: true,
        margin: { top: 0, right: 0, bottom: 0, left: 0 }
    });

    await browser.close();
    console.log('PDF gerado com sucesso em:', pdfPath);
})();
