cd /d %0\..
rd /s export
svn copy https://www.seasar.org/svn/sandbox/koropokkur.net/trunk/koropokkur.net https://www.seasar.org/svn/sandbox/koropokkur.net/tags/koropokkur.net_0.2.5 -m "ver0.2.5ÉäÉäÅ[ÉX"
svn export https://www.seasar.org/svn/sandbox/koropokkur.net/tags/koropokkur.net_0.2.5 ./export