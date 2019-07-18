using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Copyright (c) 2019 Lomaku Technologies.
 * All Rights Reserved.
 * 40 Estate Road, Angelo Goveya, Limbe.
 * P.O. Box 30653, Lilongwe 3.
 * +265 999 388 747
 * +265 888 676 896
 * https://github.com/LomakuIT
 * admin@lomakuit.com
 *
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 * this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its contributors
 * may be used to endorse or promote products derived from this software
 * without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE
 */
namespace msce_csv_json_extracter.classes
{/* Created by Chief Wiz on 07-06-2019
     * app constants class to keep constants, URLs, keys, TAGs, regex etc
     */
    class AppConstants
    {
        public const String REGEX_MSCE_RESULTS_2 = @"(?<candidateCode>[M][\s][0-9]{4}[\s][\/][\s][0-9]{3})[\s](?<gender>[M,F]+)[\s](?<firstname>[A-Z]+)[\s](?<surname>[A-Z,\s]+)[\s][MSCE].*(?<candidateCode1>[M][\s][0-9]{4}[\s][\/][\s][0-9]{3})[\s](?<gender1>[M,F]+)[\s](?<firstname1>[A-Z]+)[\s](?<surname1>[A-Z,\s]+)[\s][MSCE].*";
        public const String REGEX_MSCE_RESULTS_1 = @"(?<candidateCode>[M][\s][0-9]{4}[\s][\/][\s][0-9]{3})[\s](?<gender>[M,F]+)[\s](?<firstname>[A-Z]+)[\s](?<surname>[A-Z]+)[\s]+MSCE.*";
        public const String REGEX_MSCE_RESULTS_TRIMMED = "";
        public const String REGEX_MSCE_CENTRE_NAME= @".*CENTRE[\s]NO:[\s](?<centre>[A-Z,a-z,\,,(,),\\,\/,\s,\',-]+).*";
        public const String REGEX_MSCE_CENTRE_NO = @"^(?<centreNumber>[0-9]{4})$";
        public const String REGEX_MSCE_DISTRICT_NAME = @".*DISTRICT[\s]NAME:[\s](?<district>[A-Z,a-z,\s,\']+).*";
        public const String MSCE_FULL = "MALAWI SCHOOL CERTIFICATE OF EDUCATION";
        public const String TAG_CANDIDATE_CODE_GROUP = "candidateCode";
        public const String TAG_GENDER_CODE_GROUP = "gender";
        public const String TAG_FIRSTNAME_CODE_GROUP = "firstname";
        public const String TAG_SURNAME_CODE_GROUP = "surname";
        public const String TAG_CANDIDATE1_CODE_GROUP = "candidateCode1";
        public const String TAG_GENDER1_CODE_GROUP = "gender1";
        public const String TAG_FIRSTNAME1_CODE_GROUP = "firstname1";
        public const String TAG_SURNAME1_CODE_GROUP = "surname1";
        public const String TAG_DISTRICT_CODE_GROUP = "district";
        public const String TAG_CENTRE_CODE_GROUP = "centre";
        public const String TAG_CENTRE_NUMBER_CODE_GROUP = "centreNumber";

        public const String TAG_DISTRICT_NAME = "name";
        public const String TAG_REGION = "region";
        public const String TAG_REGION_ID = "region_id";

        public const String TAG_CANDIDATE_NUMBER = "number";
        public const String TAG_CENTRE_CODE = "centre_code";
        public const String TAG_CENTRE_ID = "centre_id";
        public const String TAG_GENDER = "gender";
        public const String TAG_CANDIDATE_NAME = "name";
        public const String TAG_CENTRE_NAME = "name";
        public const String TAG_CODE = "code";
        public const String TAG_DISTRICT = "district";
        public const String TAG_SCHOOL_ID = "school_id";
        public const String TAG_SCHOOL_NAME = "name";
        public const String TAG_DISTRICT_ID = "district_id";

        public const String MESSAGE_FILE_DIALOG_NONE_SELECTED = "No files have been selected for extraction. Please select one or more CSV files for extraction";
        public const String CAPTION_FILE_DIALOG_NONE_SELECTED = "No File(s) Selected";

        public static String MESSAGE_DEVELOPERS_APP = String.Format("MSCE CSV-JSON Extracter was developed by Lomaku Technologies T/A Lomaku IT, Malawi.\n\nAll rights reserved {0} {1}", "\u00a9", DateTime.Now.Year.ToString());
        public const String CAPTION_DEVELOPERS_APP = "MSCE CSV-JSON Extracter";

        public static String MESSAGE_CONTACT_US = String.Format("Lomaku IT can be directly contacted via WhatsApp {0}\nor via email {1}\nor alternatively on our Facebook page: /{2}", "+265 888 676 896", "msce@lomakuit.com","lomakuit");
        public const String CAPTION_CONTACT_US = "Contact Lomaku Technologies";

        public const String NAME_DISTRICT_BLANTYRE = "BLANTYRE";
        public const String NAME_DISTRICT_BALAKA = "BALAKA";
        public const String NAME_DISTRICT_CHIKHWAWA = "CHIKHWAWA";
        public const String NAME_DISTRICT_CHIRADZULU = "CHIRADZULU";
        public const String NAME_DISTRICT_DEDZA = "DEDZA";
        public const String NAME_DISTRICT_DOWA = "DOWA";
        public const String NAME_DISTRICT_LIKOMA = "LIKOMA";
        public const String NAME_DISTRICT_KASUNGU = "KASUNGU";
        public const String NAME_DISTRICT_MACHINGA = "MACHINGA";
        public const String NAME_DISTRICT_CHITIPA = "CHITIPA";
        public const String NAME_DISTRICT_KARONGA = "KARONGA";
        public const String NAME_DISTRICT_MWANZA = "MWANZA";
        public const String NAME_DISTRICT_MULANJE = "MULANJE";
        public const String NAME_DISTRICT_MZIMBA = "MZIMBA";
        public const String NAME_DISTRICT_MZUZU = "MZUZU";
        public const String NAME_DISTRICT_NENO = "NENO";
        public const String NAME_DISTRICT_MCHINJI = "MCHINJI";
        public const String NAME_DISTRICT_NKHATA = "NKHATA";
        public const String NAME_DISTRICT_NKHOTAKOTA = "NKHOTAKOTA";
        public const String NAME_DISTRICT_NSANJE = "NSANJE";
        public const String NAME_DISTRICT_PHALOMBE = "PHALOMBE";
        public const String NAME_DISTRICT_MANGOCHI = "MANGOCHI";
        public const String NAME_DISTRICT_SALIMA = "SALIMA";
        public const String NAME_DISTRICT_NTCHEU = "NTCHEU";
        public const String NAME_DISTRICT_THYOLO = "THYOLO";
        public const String NAME_DISTRICT_ZOMBA = "ZOMBA";
        public const String NAME_DISTRICT_NTCHISI = "NTCHISI";
        public const String NAME_DISTRICT_RUMPHI = "RUMPHI";
        public const String NAME_DISTRICT_LILONGWE = "LILONGWE";

    }
}
