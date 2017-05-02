const exec = require('shelljs').exec

const isMac = /^darwin/.test(process.platform)
const args = process.argv.slice(2).join(' ')

let cmd = 'powershell ./build.ps1 '

if (isMac) {
  cmd = 'sh ./build.sh '
}

exec(`${cmd} ${args}`, (code, stdout, stderr) => {
  console.log(stdout)

  if(stderr != null) {
    console.log(stdout)
  }
})
