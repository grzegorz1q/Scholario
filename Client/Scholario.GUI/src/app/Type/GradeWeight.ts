export const GradeWeight = {
    Sprawdzian: 5,
    Kartkówka: 4,
    Odpowiedź: 3,
    PracaDomowa: 2,
    PracaDodatkowa: 1
  } as const;
  
  // Typy wartości
  export type GradeWeightType = keyof typeof GradeWeight;
  